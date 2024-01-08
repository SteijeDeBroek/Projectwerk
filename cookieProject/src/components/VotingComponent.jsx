import React, { useState, useEffect } from "react";
import "animate.css";
const VotingComponent = () => {
  const [voted, setVoted] = useState(false);
  const [voteCount, setVoteCount] = useState(0);
  const [chefTitle, setChefTitle] = useState("");

  const handleVote = (voteType) => {
    if (voted) {
      alert("You have already voted!");
      return;
    }

    // replace with database interaction
    if (voteType === "like") {
      // Call database API to increment the like count
    } else if (voteType === "dislike") {
      // Call database API to increment the dislike count
    }

    setVoted(true);
    setVoteCount((prevCount) => prevCount + 1);
  };

  const handleNext = () => {
    // Placeholder logic to fetch and display the next dish
    // Example: api.fetchNextDish();
    setVoted(false); // Reset voted state for the new dish
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

  return (
    <div className="max-w-md mx-auto mt-10 p-8 border rounded-lg shadow-lg bg-white">
      <div className="mb-4">
        <p className="text-xl font-bold text-center text-blue-800">VotePage</p>
        <img
          src="afbeelding-url"
          alt="Vote"
          className="w-full h-32 object-cover mb-4 rounded"
        />
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
