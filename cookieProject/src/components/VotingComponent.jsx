import { useState, useEffect } from "react";
import "animate.css";
import {
  getTodosByUserId,
  getRecipeById,
  postVote,
  getRandomImageByRecipeId,
  getCategoryById,
  deleteTodos,
} from "../api";

const VotingComponent = () => {
  const [voteCount, setVoteCount] = useState(0);
  const [chefTitle, setChefTitle] = useState("");
  const [todos, setTodos] = useState([]);
  const [recipe, setRecipe] = useState("");
  const [image, setImage] = useState("");
  const [category, setCategory] = useState("");
  const [currentIndex, setCurrentIndex] = useState(0);
  const [loadingRandomTodos, setLoadingRandomTodos] = useState(true);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchRandomizedTodos = async () => {
      try {
        const randomTodosResponse = await getTodosByUserId(3); // replace with user id
        setTodos(randomTodosResponse);
        setLoadingRandomTodos(false);
      } catch (err) {
        console.error("Error fetching todos:", err);
        setError(err);
        setIsLoading(false);
      }
    };
    fetchRandomizedTodos();
  }, []);

  useEffect(() => {
    const fetchImageAndTitle = async () => {
      try {
        const image = await getRandomImageByRecipeId(
          todos[currentIndex].recipeId
        );
        const recipe = await getRecipeById(todos[currentIndex].recipeId);
        const category = await getCategoryById(recipe.categoryId);

        setImage(image);
        setRecipe(recipe);
        setCategory(category);
        setIsLoading(false);
      } catch (err) {
        console.error("Error fetching image and title:", err);
      }
    };

    if (!loadingRandomTodos) {
      fetchImageAndTitle();
    }
  }, [todos, currentIndex, loadingRandomTodos]);

  const handleVote = async (vote) => {
    try {
      const postData = {
        vote1: vote,
        recipeId: todos[currentIndex].recipeId,
        userId: todos[currentIndex].userId,
      };
      await postVote(postData);
      await deleteTodos(postData.recipeId, postData.userId);

      setTodos((td) =>
        td.filter((i) => i.recipeId != todos[currentIndex].recipeId)
      );
      setVoteCount((prevCount) => prevCount + 1);
      handleNext();
    } catch (error) {
      console.error("Error posting vote:", error);
    }
  };

  const handleNext = () => {
    setCurrentIndex((prevIndex) => (prevIndex + 1) % todos.length);
  };

  useEffect(() => {
    if (voteCount === 10) {
      setChefTitle("Hobby-kok");
    } else if (voteCount === 20) {
      setChefTitle("Sous-Chef");
    } else if (voteCount === 50) {
      setChefTitle("Sterren-chef!");
    } else if (voteCount === 100) {
      setChefTitle("Tim Boury!");
    }
  }, [voteCount]);

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  if (todos.length === 0) {
    return <div>You casted all your votes!</div>;
  }

  return (
    <div className="max-w-md mx-auto mt-10 p-8 border rounded-lg shadow-lg bg-white">
      <div className="mb-4">
        <p className="text-3xl font-bold text-center text-blue-800 mb-4">
          Stem op {category.name}
        </p>
        <img
          src={"data:image/jpeg;base64," + image.base64Image}
          alt="Vote"
          className="w-full h-32 object-cover mb-4 rounded"
        />
        <p style={{ fontWeight: 700, textAlign: "center" }}>{recipe.title}</p>
      </div>

      <div className="flex items-center justify-between">
        <button
          onClick={() => handleVote(true)}
          className={`bg-green-500 w-1/3 rounded-md text-white px-4 py-2 mr-2 transition duration-300 ease-in-out hover:bg-green-600 animate_animated animate-pulse`}
        >
          Like
        </button>
        <button
          onClick={() => handleVote(false)}
          className={`bg-red-500 w-1/3 rounded-md text-white px-4 py-2 transition duration-300 ease-in-out hover:bg-red-600 animate_animated animate-pulse`}
        >
          Dislike
        </button>
      </div>
      <div className="mt-4">
        <button
          onClick={handleNext}
          className="w-full bg-blue-500 rounded-md text-white px-4 py-2 transition duration-300 ease-in-out hover:bg-blue-600"
        >
          Next
        </button>
      </div>
      <div className="mt-4 text-center text-gray-600">
        Votes this session: {voteCount}
      </div>
      {chefTitle && (
        <div className="mt-2 text-center text-green-600 font-bold">
          {chefTitle}
        </div>
      )}
    </div>
  );
};

export default VotingComponent;
