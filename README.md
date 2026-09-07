# Airline Booking — Planning & Documentation

Central project repository: backlog, tasks, and links to all team resources.

**Stack:** .NET (microservices) · React + MUI (web) · Kubernetes (deployment)

---

## 📌 Resources

| Resource | Link | Purpose |
|----------|------|---------|
| Excalidraw | [open](https://excalidraw.com/) | Architecture diagrams, sketches |
| Figma | [open](https://www.figma.com/) | Design mockups (web & mobile) |
| Task board | [open](https://gitlab.com/groups/airline-booking/-/boards) | Team kanban board |
| Backlog | [open](https://gitlab.com/groups/airline-booking/-/work_items) | Full list of tasks |

---

## 🗂 Project Structure

- **backend/** — .NET microservices
  - `identity` — authentication, users
  - `flights` — flight search
  - `booking` — bookings
  - `payments` — payments
- **frontend/** — clients
  - `user-ui` — React + MUI
  - `mobile-app` (maybe later)
- **infrastructure/** — deployment, Helm charts, CI templates

---

## 🚀 Getting Started

**Requirements:**
- .NET SDK 8+
- Node.js 20+
- Docker

_Setup instructions will be added as services are created._

---

## 🤝 Team Conventions

**Branches:**
- `main` — stable
- `dev` — integration
- `feature/<issue-number>-short-description` — features

**Commits:** short and clear, with the issue number (`#5`).

**Working with tasks:** each task → its own branch → Merge Request with `Closes #<number>`.