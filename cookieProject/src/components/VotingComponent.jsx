import { useState, useEffect } from "react";
import "animate.css";
import {
  getTodosByUserId,
  getRecipeById,
  postVote,
} from "../api";

const VotingComponent = () => {
  const [voted, setVoted] = useState(false);
  const [voteCount, setVoteCount] = useState(0);
  const [chefTitle, setChefTitle] = useState("");
  const [todos, setTodos] = useState([]);
  const [recipe, setRecipe] = useState("");
  const [image, setImage] = useState("");
  const [currentIndex, setCurrentIndex] = useState(0);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchRandomizedTodos = async () => {
      try {
        const randomTodosResponse = await getTodosByUserId(3);
        setTodos(randomTodosResponse);
        setIsLoading(false);
      } catch (err) {
        console.error("Error fetching todos:", err);
        setError(err);
        setIsLoading(false);
      }
    };
    fetchRandomizedTodos();
  }, [isLoading]);

  useEffect(() => {
    const fetchImageAndTitle = async () => {
      try {
        const image = await getRandomImageByRecipeId(
          todos[currentIndex].recipeId
        );
        const recipe = await getRecipeById(todos[currentIndex].recipeId);

        // Use the fetched data as needed
        setImage(image);
        setRecipe(recipe);
      } catch (err) {
        console.error("Error fetching image and title:", err);
      }
    };

    if (!isLoading) {
      fetchImageAndTitle();
    }
  }, [todos, currentIndex, isLoading]);

  const handleVote = async (voteType) => {
    if (voted) {
      alert("You have already voted!");
      return;
    }

    try {
      // replace with database interaction
      if (voteType === "like") {
        await postVote({
          vote1: true,
          timestamp: Date.now(),
          recipeId: todos[currentIndex].recipeId,
          userId: todos[currentIndex].userId,
        });
      } else if (voteType === "dislike") {
        // Call database API to increment the dislike count
      }

      setVoted(true);
      setVoteCount((prevCount) => prevCount + 1);
    } catch (error) {
      console.error("Error posting vote:", error);
    }
  };

  const handleNext = () => {
    // Increment the index to display the next image and title
    setCurrentIndex((prevIndex) => (prevIndex + 1) % todos.length);
    setVoted(false); // Reset voted state for the new dish
  };

  useEffect(() => {
    // Update chef title based on vote count
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

  return (
    <div className="max-w-md mx-auto mt-10 p-8 border rounded-lg shadow-lg bg-white">
      <div className="mb-4">
        <p className="text-xl font-bold text-center text-blue-800">VotePage</p>
        <img
          src={"data:image/jpeg;base64," + image.base64Image}
          alt="Vote"
          className="w-full h-32 object-cover mb-4 rounded"
        />
        <p>{recipe.title}</p>
      </div>

      <div className="flex items-center justify-between">
        <button
          onClick={() => handleVote("like")}
          className={`${
            voted ? "bg-gray-500 cursor-not-allowed" : "bg-green-500"
          } w-1/3 rounded-md text-white px-4 py-2 mr-2 transition duration-300 ease-in-out hover:bg-green-600 animate_animated animate-pulse`}
          disabled={voted}
        >
          Like
        </button>
        <button
          onClick={() => handleVote("dislike")}
          className={`${
            voted ? "bg-gray-500 cursor-not-allowed" : "bg-red-500"
          } w-1/3 rounded-md text-white px-4 py-2 transition duration-300 ease-in-out hover:bg-red-600 animate_animated animate-pulse`}
          disabled={voted}
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
