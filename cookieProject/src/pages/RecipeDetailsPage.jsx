import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getRecipeById } from "../api";

const RecipeDetailsPage = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [recipe, setRecipe] = useState(null);

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
        {recipe.title}
      </h1>
      <p
        style={{
          padding: "2rem",
          fontSize: "1rem",
          textAlign: "center",
          color: "black",
        }}
      >
        {recipe.description}
      </p>
    </div>
  );
};

export default RecipeDetailsPage;
