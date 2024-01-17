/* eslint-disable react/jsx-key */
/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import "../css/Competities.css";
import { getSortedWinningRecipes, getMostRecentCategories } from "../api";
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
        const competitionsResponse = await getMostRecentCategories(3);
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
    <div className="pl-10 mb-10 flex-col">
      <div className="mb-10 p-10 inline-block border-4 border-blue-800 rounded-2xl">
        <p className="text-5xl font-bold text-blue-800">
          Afgelopen competities
        </p>
      </div>
      <div>
        {competities.map((c, index) => {
          return (
            <CompetitiesBoxComponent
              key={"Category" + c.categoryId}
              competitie={c}
              recipes={recipes[index]}
              borderColor={borderColors[index]}
              backgroundColor={backgroundColors[index]}
              index={index}
            />
          );
        })}
      </div>
    </div>
  );
};

export default CompetitiesComponent;
