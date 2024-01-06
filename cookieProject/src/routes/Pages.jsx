import { Navigate } from "react-router-dom";

import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import UploadPage from "../pages/UploadPage";
import VotePage from "../pages/VotePage";
import NotFoundPage from "../pages/NotFoundPage";
import RegisterPage from "../pages/RegisterPage";

export const pages = [
  {
    headerLabel: null,
    path: "/",
    element: <Navigate replace to="/home" />,
    shouldLogin: false,
  },
  {
    headerLabel: "Home",
    path: "/home",
    element: <HomePage />,
    shouldLogin: false,
  },
  {
    headerLabel: "Login",
    path: "/login",
    element: <LoginPage />,
    shouldLogin: false,
  },
  {
    headerLabel: null,
    path: "/register",
    element: <RegisterPage />,
    shouldLogin: false,
  },
  {
    headerLabel: "Upload",
    path: "/upload",
    element: <UploadPage />,
    shouldLogin: true,
  },
  {
    headerLabel: "Vote",
    path: "/vote",
    element: <VotePage />,
    shouldLogin: true,
  },
  {
    headerLabel: null,
    path: "*",
    element: <NotFoundPage />,
    shouldLogin: false,
  },
];
