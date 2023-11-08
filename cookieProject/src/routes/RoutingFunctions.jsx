import React from "react";
import { Route, Navigate } from "react-router-dom";

const loginTest = (page, key) => {
  const LoggedIn = true; // TODO: Test if logged in

  return LoggedIn ? (
    <Route path={page.path} element={page.element} key={key} />
  ) : (
    <Route
      path={page.path}
      element={<Navigate replace to="/login" />}
      key={key}
    />
  );
};

export const RouteTo = (page, key) => {
  return page.shouldLogin ? (
    loginTest(page, key)
  ) : (
    <Route path={page.path} element={page.element} key={key} />
  );
};
