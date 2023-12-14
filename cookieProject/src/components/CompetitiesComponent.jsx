/* eslint-disable react/jsx-key */
/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import "../css/Competities.css"; /* Hij leest de css file niet? */
import {
  mostRecentCategories,
  // getWinningRecipes,
  getWinningUsers,
  getImageById,
  getCategories,
  manualCategories,
  getSortedWinningRecipes,
} from "../api";
import CompetitiesBoxComponent from "./CompetitiesBoxComponent";

const images = [
  {
    recipeId: 1,
    images: [
      {
        imageId: 1,
        base64Image: "base64Image",
      },
    ],
  },
  {
    recipeId: 2,
    images: [
      {
        imageId: 2,
        base64Image: "base64Image",
      },
    ],
  },
];

const CompetitiesComponent = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [competities, setCompetities] = useState([]);
  const [images, setImages] = useState([]);
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

  // useEffect(() => {
  //   const fetchRecipes = async (id) => {
  //     try {
  //       const response = await getWinningRecipes(id);
  //       setRecipes(response);
  //     } catch (err) {
  //       console.error("error:", err);
  //     }
  //   };
  //   fetchRecipes();
  // }, []);

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

  // useEffect(() => {
  //   const fetchImage = async (id) => {
  //     try {
  //       const response = await getImageById(id);
  //       setImage(response);
  //     } catch (err) {
  //       console.error("error:", err);
  //     }
  //   };
  //   fetchImage();
  // }, []);

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

        console.log("allRecipes", allRecipes);

        setIsLoading(false);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchCompetities();
  }, [isLoading]);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  // Old code

  // const generateCompetities = () => {
  //   let allCompetities = [];
  //   // console.log(JSON.stringify(competities));
  //   for (let i = 0; i < competities.length; i++) {
  //     allCompetities.push(
  //       <div>
  //         <p>Category Id: {competities[i].categoryId}</p>
  //         <h2>Category name: {competities[i].name}</h2>
  //         <p>Recipes:</p>
  //         <ul>
  //           {recipes[i].map((r) => (
  //             <li key={r.recipeId}>{r.title}</li>
  //           ))}
  //         </ul>
  //         <br />
  //       </div>
  //     );
  //   }
  //   return allCompetities;
  // };

  return (
    <div className="pl-40">
      <p className="text-4xl font-bold text-gray-800">Competities</p>
      {/* {generateCompetities().map((c, index) => (
        <div key={index}>{c}</div>
      ))} */}
      {competities.map((c, index) => {
        return (
          <CompetitiesBoxComponent
            key={c.id}
            competitie={c}
            images={images[index]}
            recipes={recipes[index]}
            borderColor={borderColors[index]}
            backgroundColor={backgroundColors[index]}
          />
        );
      })}
    </div>
  );
};

export default CompetitiesComponent;
