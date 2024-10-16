import Home from "@/pages/HomePage/Home";
import { Layout } from "@/pages/Layout";
import { createBrowserRouter, } from "react-router-dom";
import Students from "@/pages/StudentsPage/Students";
import Lecturers from "@/pages/LecturersPage/Lecturers";
import Programmes from "@/pages/ProgrammesPage/Programmes";
import Subjects from "@/pages/SubjectsPage/Subjects";
import Groups from "@/pages/GroupsPage/Groups";
import SignUp from "@/pages/auth/SignUpPage/SignUp";
import SignIn from "@/pages/auth/SignInPage/SignIn";
import { ProtectedRoute } from "@/ProtectedRoute";
import Dashboard from "@/pages/admin/DashboardPage/Dashboard";

export function router(){
    return createBrowserRouter([
        {
            path: "/",
            Component: Layout,
            children: [
                {
                    index: true,
                    Component: Home
                },
                {
                    path: 'students',
                    element: <ProtectedRoute><Students /></ProtectedRoute>
                },
                {
                    path: 'lecturers',
                    element: <ProtectedRoute><Lecturers /></ProtectedRoute>
                },
                {
                    path: 'programmes',
                    element: <ProtectedRoute><Programmes /></ProtectedRoute>
                },
                {
                    path: 'subjects',
                    element: <ProtectedRoute><Subjects /></ProtectedRoute>
                },
                {
                    path: 'groups',
                    element: <ProtectedRoute><Groups /></ProtectedRoute>
                },
                {
                    path: 'authentication/signup',
                    Component: SignUp
                },
                {
                    path: 'authentication/signin',
                    Component: SignIn
                },
                {
                    path: 'admin/dashboard',
                    element: <ProtectedRoute><Dashboard /></ProtectedRoute>
                }
            ]
        },
    ]);
}