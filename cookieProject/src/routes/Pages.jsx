import React from "react";
import { Navigate } from "react-router-dom";

import HomePage from "../pages/HomePage";
import LoginPage from "../pages/LoginPage";
import UploadPage from "../pages/UploadPage";
import VotePage from "../pages/VotePage";
import NotFoundPage from "../pages/NotFoundPage";

export const pages = [
  {
    path: "/",
    element: <Navigate replace to="/home" />,
    shouldLogin: false,
  },
  {
    path: "/home",
    element: <HomePage />,
    shouldLogin: false,
  },
  {
    path: "/login",
    element: <LoginPage />,
    shouldLogin: false,
  },
  {
    path: "/upload",
    element: <UploadPage />,
    shouldLogin: true,
  },
  {
    path: "/vote",
    element: <VotePage />,
    shouldLogin: true,
  },
  {
    path: "*",
    element: <NotFoundPage />,
    shouldLogin: false,
  },
];
