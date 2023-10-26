import React from "react";
import { Routes } from "react-router-dom";
import { RouteTo } from "./RoutingFunctions";

import { pages } from "./Pages";

const MainRouter = () => {
  return (
    <Routes>
      {pages.map((page) => {
        return RouteTo(page);
      })}
    </Routes>
  );
};

export default MainRouter;
