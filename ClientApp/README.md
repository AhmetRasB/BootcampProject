# Bootcamp Management System - React Client

A React-based frontend for the Bootcamp Management System API.

## Features

- **Dashboard**: Overview of system statistics
- **Bootcamps**: Manage bootcamp programs
- **Applications**: Handle student applications
- **Applicants**: Manage student profiles
- **Instructors**: Manage instructor profiles
- **Login**: User authentication

## Getting Started

### Prerequisites

- Node.js (version 14 or higher)
- npm or yarn
- The backend API running on `http://localhost:5078`

### Installation

1. Navigate to the ClientApp directory:
   ```bash
   cd ClientApp
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

4. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

## Usage

### Dashboard
- View system statistics
- Seed test data with one click

### Bootcamps
- View all bootcamps
- Add new bootcamps
- Delete existing bootcamps

### Applications
- View all applications
- Create new applications
- Update application status
- Delete applications

### Applicants
- View all applicants
- Add new applicants
- Delete applicants

### Instructors
- View all instructors
- Add new instructors
- Delete instructors

### Login
- Test user authentication

## API Integration

The React app communicates with the .NET Web API through the `api.js` service file. All API calls are configured to proxy to `http://localhost:5078` during development.

## Available Scripts

- `npm start`: Runs the app in development mode
- `npm test`: Launches the test runner
- `npm run build`: Builds the app for production
- `npm run eject`: Ejects from Create React App (one-way operation)

## Technologies Used

- React 18
- React Router DOM
- Axios for API calls
- CSS for styling 