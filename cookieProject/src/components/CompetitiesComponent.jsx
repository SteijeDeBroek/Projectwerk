/* eslint-disable react/jsx-key */
/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import "../css/Competities.css"; /* Hij leest de css file niet? */
import {
  mostRecentCategories,
  getWinningRecipes,
  getWinningUsers,
  getImageById,
  getCategories,
} from "../api";
import CompetitiesBoxComponent from "./CompetitiesBoxComponent";

const CompetitiesComponent = () => {
  const [competities, setCompetities] = useState([]);
  const [image, setImage] = useState([]);
  const [recipes, setRecipes] = useState([]);
  const [users, setUsers] = useState([]);

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
    const fetchUsers = async () => {
      try {
        const response = await getWinningUsers();
        setUsers(response);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchUsers();
  }, []);

  useEffect(() => {
    const fetchRecipes = async (id) => {
      try {
        const response = await getWinningRecipes(id);
        setRecipes(response);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchRecipes();
  }, []);

  // useEffect(() => {
  //   const fetchWinningImages = async () => {
  //     try {
  //       const response = await getWinningImages();
  //       setImages(response);
  //     } catch (err) {
  //       console.error("error:", err);
  //     }
  //   };
  //   fetchWinningImages();
  // }, []);

  useEffect(() => {
    const fetchImage = async (id) => {
      try {
        const response = await getImageById(id);
        setImage(response);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchImage();
  }, []);

  useEffect(() => {
    const fetchCompetities = async () => {
      try {
        const competitionsResponse = await getCategories();
        setCompetities(competitionsResponse);

        // Initialiseer een lege array voor de recepten
        let allRecipes = [];

        // Loop door de competitiedata en haal voor elke competitie de bijbehorende recepten op
        for (const competition of competitionsResponse) {
          const winningRecipesResponse = await getWinningRecipes(
            competition.categoryId
          );
          allRecipes = [...allRecipes, ...winningRecipesResponse];
        }
        setRecipes(allRecipes);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchCompetities();
  }, []);

  return (
    <div className="pl-40">
      {competities.map((c, index) => {
        setRecipes((c) => c.recipeId);
        return (
          <CompetitiesBoxComponent
            key={c.id}
            competitie={c}
            images={recipes.map((r) => r.imageId)}
            borderColor={borderColors[index]}
            backgroundColor={backgroundColors[index]}
          />
        );
      })}

      {/* <CompetitiesBoxComponent
        competitie={competities[0]}
        images={images.slice(0, 4)}
        recipes={recipes.slice(0, 4)}
        borderColor="border-orange-400"
        backgroundColor="bg-orange-300"
      />
      <CompetitiesBoxComponent
        competitie={competities[1]}
        images={images.slice(4, 8)}
        recipes={recipes.slice(4, 8)}
        borderColor="border-green-400"
        backgroundColor="bg-green-300"
      />
      <CompetitiesBoxComponent
        competitie={competities[2]}
        images={images.slice(8, 12)}
        recipes={recipes.slice(8, 12)}
        borderColor="border-blue-400"
        backgroundColor="bg-blue-300"
      /> */}
    </div>
  );
};

export default CompetitiesComponent;
