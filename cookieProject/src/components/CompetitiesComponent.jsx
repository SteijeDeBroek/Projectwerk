/* eslint-disable react/jsx-key */
/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import "../css/Competities.css";
import { manualCategories, getSortedWinningRecipes } from "../api";
import CompetitiesBoxComponent from "./CompetitiesBoxComponent";

const CompetitiesComponent = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [competities, setCompetities] = useState([]);
  const [recipes, setRecipes] = useState([]);

  const borderColors = [
    "border-green-400",
    "border-teal-400",
    "border-indigo-400",
    "border-purple-400",
    "border-pink-400",
    "border-red-400",
    "border-orange-400",
    "border-amber-400",
    "border-lime-400",
    "border-yellow-400",
  ];

  const backgroundColors = [
    "bg-green-300",
    "bg-teal-300",
    "bg-indigo-300",
    "bg-purple-300",
    "bg-pink-300",
    "bg-red-300",
    "bg-orange-300",
    "bg-amber-300",
    "bg-lime-300",
    "bg-yellow-300",
  ];

  useEffect(() => {
    const fetchCompetities = async () => {
      try {
        const competitionsResponse = await manualCategories();
        setCompetities(competitionsResponse);

        // Initialiseer een lege array voor de recepten
        let allRecipes = [];

        // Loop door de competitiedata en haal voor elke competitie de bijbehorende recepten op
        for (const competition of competitionsResponse) {
          const winningRecipesResponse = await getSortedWinningRecipes(
            competition.categoryId
          );
          allRecipes.push(winningRecipesResponse);
        }
        setRecipes(allRecipes);

        setIsLoading(false);
      } catch (err) {
        console.error("Error fetching competities:", err);
        setError(err);
        setIsLoading(false);
      }
    };
    fetchCompetities();
  }, [isLoading]);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <div className="px-4 lg:px-10 space-y-5">
      {" "}
      <p className="text-4xl font-bold text-gray-800">Competities</p>
      <div className="space-y-5">
        {" "}
        {competities.map((c, index) => {
          return (
            <CompetitiesBoxComponent
              key={"Category" + c.categoryId}
              competitie={c}
              recipes={recipes[index]}
              borderColor={borderColors[index]}
              backgroundColor={backgroundColors[index]}
            />
          );
        })}
      </div>
    </div>
  );
};

export default CompetitiesComponent;
