param([ValidateSet('Start','Stop','Status','Connect','Credential','Configure')][string]$Action = 'Status')

# Автор: DementevMaxim
# Дата изменения: 2026-10-19
# EULA: legal/EULA.txt
# NOTICE: legal/NOTICE.txt

$ErrorActionPreference = 'Stop'
$taskBase = Join-Path $env:LOCALAPPDATA 'BookingMicroService'
$taskBin = Join-Path $taskBase 'PostgreSQL17\pgsql\bin'
$taskData = Join-Path $taskBase 'pgdata17'
if (!(Test-Path -LiteralPath "$taskBin\pg_ctl.exe")) { throw 'Local portable PostgreSQL is not installed for this Windows user.' }
switch ($Action) {
    'Start' {
        & "$taskBin\pg_ctl.exe" status -D $taskData
        if ($LASTEXITCODE -ne 0) { & "$taskBin\pg_ctl.exe" start -D $taskData -l "$taskBase\postgresql.log" -o '-h 127.0.0.1 -p 5432' -w }
    }
    'Stop' { & "$taskBin\pg_ctl.exe" stop -D $taskData -m fast -w }
    'Status' { & "$taskBin\pg_ctl.exe" status -D $taskData }
    'Credential' {
        # Explicit interactive retrieval only; the encrypted credential belongs to this Windows user.
        Import-Clixml -LiteralPath "$taskBase\booking-owner.credential.xml"
        return
    }
    'Configure' {
        $taskCredential = Import-Clixml -LiteralPath "$taskBase\booking-owner.credential.xml"
        $taskConnection = [System.Data.Common.DbConnectionStringBuilder]::new()
        $taskConnection['Host'] = '127.0.0.1'
        $taskConnection['Port'] = 5432
        $taskConnection['Database'] = 'booking_db'
        $taskConnection['Username'] = $taskCredential.UserName
        $taskConnection['Password'] = $taskCredential.GetNetworkCredential().Password
        $env:BOOKING_DATABASE_CONNECTION_STRING = $taskConnection.ConnectionString
        Write-Output 'Connection configured for this shell and processes launched from it; password was not printed or saved in the repository.'
        return
    }
    'Connect' {
        $taskPreviousPassword = $env:PGPASSWORD
        try {
            $taskCredential = Import-Clixml -LiteralPath "$taskBase\booking-owner.credential.xml"
            $env:PGPASSWORD = $taskCredential.GetNetworkCredential().Password
            & "$taskBin\psql.exe" -h 127.0.0.1 -p 5432 -U booking_owner -d booking_db
        } finally { $env:PGPASSWORD = $taskPreviousPassword }
    }
}
if ($LASTEXITCODE -ne 0) { throw "PostgreSQL command exited with code $LASTEXITCODE" }
