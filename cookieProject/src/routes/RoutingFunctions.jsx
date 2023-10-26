import React from "react";
import { Route } from "react-router-dom";

const LoggedIn = true; // TODO: Test if logged in

const IsValidLogin = (pageElement) => {
  return LoggedIn ? pageElement : <LoginPage />; // FIXME: This should not return LoginPage, it should navigate to LoginPage instead
};

export const RouteTo = (page, key) => {
  const pageElement = page.element;
  const routePageElement = page.shouldLogin
    ? IsValidLogin(pageElement)
    : pageElement;

  return <Route path={page.path} element={routePageElement} key={key} />;
};
