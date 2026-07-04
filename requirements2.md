# NerdMeter - Current Project Description

## What Is This App?

NerdMeter is a multi-page web app that lets users take a humorous quiz, submit their name, and see a leaderboard ranked by nerd score.

The app combines a static front end with a small C# .NET Minimal API backend. The backend calculates quiz scores, stores leaderboard entries in memory, and returns plain text data for the leaderboard and latest score display.

---

## Technologies Used

| Technology | Role |
| :---- | :---- |
| HTML5 | Page structure and quiz/leaderboard forms |
| CSS3 | Shared visual style, layout, responsive behavior |
| JavaScript (vanilla) | Loads shared header, fetches score and leaderboard data |
| C# with .NET Minimal API | Serves files, calculates scores, stores in-memory leaderboard |

No client framework is used. Server logic is implemented in one C# file with direct route handlers.

---

## Graphical Design Considerations

The site uses a custom visual identity with:

- A large branded header image and shared navigation.
- A dark gradient page background with bright card-style content sections.
- Orange accent styling for key score information.
- A responsive layout that adapts navigation and content spacing on smaller screens.

---

## Pages and What They Do

### Home (index.html)

Landing page for the app.

- Displays an intro message and site tone.
- Includes a Start the Quiz button.
- Button navigates users directly to the test page.

---

### About (about.html)

Short project narrative page.

- Explains, in informal language, why an About page exists.
- Reinforces the humorous voice of the project.

---

### Test (testme.html)

Main interaction page where users submit quiz answers.

- Contains six multiple-choice question groups.
- Each question uses radio button options.
- Includes a required name field.
- Form submits to POST /api/add_nerd.
- On submit, backend calculates score and redirects user to the leaderboard page.

---

### Leaderboard (nerdboard.html)

Result and ranking page.

- Shows the latest submitted score as a highlighted percentage.
- Displays full ranking list in a read-only textarea.
- Ranking data is loaded from backend on page load.
- Includes an external follow-up questionnaire link.

---

### Legacy Pages Still Present (requirements.html, examples.html)

These pages still exist in the project folder but are not part of the main four-link navigation.

- They contain content from the earlier Internet Project Guide version.
- Some legacy JavaScript/API features referenced there are currently disabled or removed.

---

## Backend API Routes

| Method | Route | Purpose |
| :---- | :---- | :---- |
| GET | /api/nerds | Returns leaderboard text, one line per user |
| GET | /api/score | Returns last submitted score |
| POST | /api/add_nerd | Calculates score, inserts user into sorted leaderboard, redirects to leaderboard page |
| POST | /api/score | Calculates and returns score from submitted answers |

---

## Behavior That Applies to Every Main Page

### Shared Header and Navigation

- Header markup is stored in a separate file and loaded into each page by JavaScript.
- Navigation links are Home, About, Test, Leaderboard.

### Shared Footer

- Main pages include the same footer phrase.

### Shared Script

- A single JavaScript file is loaded on all pages.
- Header loading runs on all pages.
- Score and leaderboard fetches run only when related elements exist.

### Responsive Layout

- Navigation wraps and adjusts on smaller screens.
- Section spacing, heading scale, and button sizing adapt for mobile widths.

---

## Data and State

- Leaderboard data is held in server memory as an array of name-score pairs.
- Initial seeded entries are present at server startup.
- New entries are inserted in descending score order.
- Latest score is stored in memory and returned by GET /api/score.
- All in-memory data resets when the server restarts.
- There is no database persistence.

---

## User Interactions

| Interaction | Where | Result |
| :---- | :---- | :---- |
| Click Start the Quiz | Home | Navigates to test page |
| Select quiz answers and enter name, then submit | Test | Sends form to backend, computes score, redirects to leaderboard |
| Load leaderboard page | Leaderboard | Fetches latest score and leaderboard text from backend |
| Click follow-up questionnaire link | Leaderboard | Opens external questionnaire in new tab |
| Resize browser window | All main pages | Layout adapts for readability and touch-friendly navigation |

---
