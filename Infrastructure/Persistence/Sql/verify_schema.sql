-- Автор: DementevMaxim
-- Дата изменения: 2026-10-19
-- EULA: legal/EULA.txt
-- NOTICE: legal/NOTICE.txt
-- Run inside a transaction; always roll back the test data.
DO $$
DECLARE b1 bigint; b2 bigint; p1 bigint; p2 bigint; r1 bigint; r2 bigint;
        f1 bigint; f2 bigint; i1 bigint; v1 bigint; t1 bigint;
BEGIN
    INSERT INTO bookings(user_id,offer_id,supplier_id,provider_offer_id,booking_code,create_idempotency_key,
      booking_status,payment_status,issuance_status,total_amount,currency,contact_email,version)
    VALUES(1,101,'test','offer-1','test-order-1','create-1','Draft','NotStarted','NotStarted',100,'RUB','test@example.invalid',1)
    RETURNING booking_id INTO b1;
    INSERT INTO bookings(user_id,offer_id,supplier_id,provider_offer_id,booking_code,create_idempotency_key,
      booking_status,payment_status,issuance_status,total_amount,currency,contact_email,version)
    VALUES(2,102,'test','offer-2','test-order-2','create-2','Draft','NotStarted','NotStarted',100,'RUB','test@example.invalid',1)
    RETURNING booking_id INTO b2;
    INSERT INTO booking_passengers(booking_id,first_name,last_name,date_of_birth,gender,citizenship_code,document_type,document_number_encrypted,document_issuing_country)
    VALUES(b1,'Test','One','2000-01-01','Male','RUS','Passport','test-ciphertext','RUS') RETURNING booking_passenger_id INTO p1;
    INSERT INTO booking_passengers(booking_id,first_name,last_name,date_of_birth,gender,citizenship_code,document_type,document_number_encrypted,document_issuing_country)
    VALUES(b2,'Test','Two','2000-01-01','Male','RUS','Passport','test-ciphertext','RUS') RETURNING booking_passenger_id INTO p2;
    INSERT INTO booking_routes(booking_id,sequence_number) VALUES(b1,1) RETURNING booking_route_id INTO r1;
    INSERT INTO booking_routes(booking_id,sequence_number) VALUES(b2,1) RETURNING booking_route_id INTO r2;
    INSERT INTO booking_flights(booking_id,booking_route_id,provider_segment_id,sequence_number,departure_airport,arrival_airport,departure_at,arrival_at,airline_code,flight_number)
    VALUES(b1,r1,'segment-1',1,'SVO','LED','2026-11-01 10:00Z','2026-11-01 12:00Z','TEST','1') RETURNING booking_flight_id INTO f1;
    INSERT INTO booking_flights(booking_id,booking_route_id,provider_segment_id,sequence_number,departure_airport,arrival_airport,departure_at,arrival_at,airline_code,flight_number)
    VALUES(b2,r2,'segment-2',1,'SVO','LED','2026-11-01 10:00Z','2026-11-01 12:00Z','TEST','2') RETURNING booking_flight_id INTO f2;
    INSERT INTO booking_items(booking_id,booking_passenger_id,booking_route_id) VALUES(b1,p1,r1) RETURNING booking_item_id INTO i1;
    INSERT INTO reservations(booking_id,supplier_id,idempotency_key,status,version)
    VALUES(b1,'test','reserve-1','Pending',1) RETURNING reservation_id INTO v1;
    UPDATE reservations SET status='Confirmed',provider_booking_id='provider-1',confirmed_at=now(),expires_at=now()+interval '1 hour',total_price_amount=100,total_price_currency='RUB' WHERE reservation_id=v1;
    INSERT INTO reservation_passengers(booking_id,reservation_id,booking_passenger_id,provider_passenger_id) VALUES(b1,v1,p1,'provider-passenger-1');
    INSERT INTO e_tickets(booking_id,booking_item_id,booking_route_id,reservation_id,ticket_number,issuer_code,status,price_amount,price_currency,issued_at)
    VALUES(b1,i1,r1,v1,'test-ticket-1','TEST','Active',100,'RUB',now()) RETURNING e_ticket_id INTO t1;
    INSERT INTO e_ticket_segments(booking_id,booking_route_id,e_ticket_id,booking_flight_id) VALUES(b1,r1,t1,f1);
    INSERT INTO outbox_messages(booking_id,message_type,payload,occurred_at,attempts) VALUES(b1,'BookingCompleted','{}',now(),0);
    INSERT INTO inbox_messages(source,external_message_id,processed_at) VALUES('Payment','event-1',now());
    BEGIN
        INSERT INTO booking_items(booking_id,booking_passenger_id,booking_route_id) VALUES(b1,p2,r1);
        RAISE EXCEPTION 'Cross-order passenger accepted';
    EXCEPTION WHEN foreign_key_violation THEN NULL; END;
    BEGIN
        INSERT INTO e_ticket_segments(booking_id,booking_route_id,e_ticket_id,booking_flight_id) VALUES(b1,r1,t1,f2);
        RAISE EXCEPTION 'Cross-order segment accepted';
    EXCEPTION WHEN foreign_key_violation THEN NULL; END;
    BEGIN
        INSERT INTO reservations(booking_id,supplier_id,idempotency_key,status,version) VALUES(b1,'test','reserve-1','Pending',1);
        RAISE EXCEPTION 'Duplicate reservation accepted';
    EXCEPTION WHEN unique_violation THEN NULL; END;
    BEGIN
        INSERT INTO inbox_messages(source,external_message_id,processed_at) VALUES('Payment','event-1',now());
        RAISE EXCEPTION 'Duplicate event accepted';
    EXCEPTION WHEN unique_violation THEN NULL; END;
    BEGIN
        UPDATE reservations SET total_price_currency=NULL WHERE reservation_id=v1;
        RAISE EXCEPTION 'Incomplete price accepted';
    EXCEPTION WHEN check_violation THEN NULL; END;
    BEGIN
        UPDATE bookings SET booking_status='wrong' WHERE booking_id=b1;
        RAISE EXCEPTION 'Invalid status accepted';
    EXCEPTION WHEN check_violation THEN NULL; END;
    BEGIN
        UPDATE bookings SET total_amount=-1 WHERE booking_id=b1;
        RAISE EXCEPTION 'Negative price accepted';
    EXCEPTION WHEN check_violation THEN NULL; END;
    BEGIN
        INSERT INTO e_tickets(booking_id,booking_item_id,booking_route_id,reservation_id,ticket_number,issuer_code,status,price_amount,price_currency,issued_at)
        VALUES(b1,i1,r1,v1,'test-ticket-1','TEST','Active',100,'RUB',now());
        RAISE EXCEPTION 'Duplicate ticket accepted';
    EXCEPTION WHEN unique_violation THEN NULL; END;
    RAISE NOTICE 'Booking schema smoke checks passed (valid chain, FK isolation, deduplication, statuses and money).';
END $$;
