import React from "react";

// if recipe has not been voted on, show here and add votes
const votingComponent = () => {
  return (
    <>
      <div className=" mt-10 ml-10 mr-10 border p-10 border-blue-300 flex items-center flex-col">
        <div>
          <img src="afbeelding-url" alt="Vote" />
          <p>VotePage</p>
        </div>
        <div className="flex items-center mt-4">
          <div className="flex-1 flex justify-end">
            <button className="bg-green-500 rounded shadow-xl hover:bg-green-600 text-white px-4 py-2 mr-2">
              Like
            </button>
            <button className="bg-red-500 rounded shadow-xl hover:bg-red-600 text-white px-4 py-2">
              Dislike
            </button>
          </div>
        </div>
      </div>
    </>
  );
};

export default votingComponent;
