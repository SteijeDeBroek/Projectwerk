import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getRecipeById } from "../api";

const RecipeDetailsPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [recipe, setRecipe] = useState(null);
  const [sentenceNumber, setSentenceNumber] = useState(0);

  const { recipeId } = useParams();

  useEffect(() => {
    getRecipeById(recipeId)
      .then((recipe) => {
        setRecipe(recipe);
      })
      .catch((error) => {
        setError(error);
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [recipeId]);

  const renderDescription = () => {
    if (!recipe || !recipe.description) {
      return null;
    }
    const sentences = recipe.description.split(".");

    return sentences.map((sentence, index) => {
      const trimmedSentence = sentence.trim();
      if (trimmedSentence) {
        return (
          <p
            key={index}
            style={{
              padding: "0.5rem",
              fontSize: "1rem",
              textAlign: "center",
              color: "black",
            }}
          >
            <span style={{ fontWeight: "bold" }}>{`${
              sentenceNumber + index + 1
            }. `}</span>
            {trimmedSentence}
          </p>
        );
      }
      return null; // negeer lege zinnen door een null te returnen na het laatste .
    });
  };

  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return <p>{error.message}</p>;
  }

  return (
    <div>
      <h1
        style={{
          backgroundImage:
            "url(https://cdn.pixabay.com/photo/2022/04/30/19/12/cooking-banner-7166200_1280.jpg)",
          backgroundSize: "cover",
          backgroundPosition: "center",
          height: "30vh",
          color: "black",
          fontSize: "4rem",
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <p className="text-center text-6xl font-bold font-serif border-black border-2 p-2 rounded-md">
          {recipe.title}
        </p>
      </h1>
      {renderDescription()}
    </div>
  );
};

export default RecipeDetailsPage;
