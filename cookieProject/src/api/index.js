import axios from "axios";

//tijd ervoor en erna loggen (tip wim!)

//definieer de functies
//categories
export const getCategories = async () => {
  const resp = await axios.get(
    "https://localhost:7170/Categories/AllCategories"
  );
  const data = await resp.data;
  return data;
};

export const getLastThreeCategories = async () => {
  console.time('getLastThreeCategories_TimerStart'); // Start de timer

  const resp = await axios.get(
    "https://localhost:7170/Categories/ThreeLastCategories"
  );

  const data = await resp.data;

  console.timeEnd('getLastThreeCategories_TimerEnd'); // Stop de timer en toon de tijd in de console

  return data;
};


export const getCategoryById = async (id) => {
  const resp = await axios.get(
    `https://localhost:7170/Categories/CategoryById/${id}`
  );
  const data = await resp.data;
  return data;
};

export const patchCategories = async (id, put) => {
  const resp = await axios.patch(
    `https://localhost:7170/Categories/Category/${id}`,
    put
  );
  return await resp.status;
};

export const deleteCategories = async (id) => {
  const resp = await axios.delete(
    `https://localhost:7170/Categories/Category/${id}`
  );
  return await resp.status;
};

export const postCategories = async (post) => {
  const resp = await axios.post(
    "https://localhost:7170/Categories/Category",
    post
  );
  return await resp.status;
};

//images
export const getImages = async () => {
  const resp = await axios.get("https://localhost:7170/Images/AllImages");
  const data = await resp.data;
  return data;
};

export const getWinningImages = async () => {
  const resp = await axios.get("https://localhost:7170/Images/WinningImages"); // images van gerechten met de meeste votes weergeven per category
  const data = await resp.data;
  return data;
};

export const getImageById = async (id) => {
  const resp = await axios.get(`https://localhost:7170/Images/ImageById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchImages = async (id, put) => {
  const resp = await axios.patch(
    `https://localhost:7170/Images/Image/${id}`,
    put
  );
  return await resp.status;
};

export const deleteImages = async (id) => {
  const resp = await axios.delete(`https://localhost:7170/Images/Image/${id}`);
  return await resp.status;
};

export const postImages = async (post) => {
  const resp = await axios.post("https://localhost:7170/Images/Image", post);
  return await resp.status;
};

//recipes
export const getRecipes = async () => {
  const resp = await axios.get("https://localhost:7170/Recipes/AllRecipes");
  const data = await resp.data;
  return data;
};

export const getWinningRecipes = async () => {
  const resp = await axios.get("https://localhost:7170/Recipes/WinningRecipes");
  const data = await resp.data;
  return data;
};

export const getRecipeById = async (id) => {
  const resp = await axios.get(
    `https://localhost:7170/Recipes/RecipeById/${id}`
  );
  const data = await resp.data;
  return data;
};

export const patchRecipes = async (id, put) => {
  const resp = await axios.patch(
    `https://localhost:7170/Recipes/Recipe/${id}`,
    put
  );
  return await resp.status;
};

export const deleteRecipes = async (id) => {
  const resp = await axios.delete(
    `https://localhost:7170/Recipes/Recipe/${id}`
  );
  return await resp.status;
};

export const postRecipes = async (post) => {
  const resp = await axios.post("https://localhost:7170/Recipes/Recipe", post);
  return await resp.status;
};

//todos
export const getTodos = async () => {
  const resp = await axios.get("https://localhost:7170/Todos/AllTodos");
  const data = await resp.data;
  return data;
};

export const getTodoById = async (id) => {
  const resp = await axios.get(`https://localhost:7170/Todos/TodoById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchTodos = async (id, put) => {
  const resp = await axios.patch(
    `https://localhost:7170/Todos/Todo/${id}`,
    put
  );
  return await resp.status;
};

export const deleteTodos = async (id) => {
  const resp = await axios.delete(`https://localhost:7170/Todos/Todo/${id}`);
  return await resp.status;
};

export const postTodos = async (post) => {
  const resp = await axios.post("https://localhost:7170/Todos/Todo", post);
  return await resp.status;
};

//Users
export const getUsers = async () => {
  const resp = await axios.get("https://localhost:7170/Users/AllUsers");
  const data = await resp.data;
  return data;
};

export const getWinningUsers = async () => {
  const resp = await axios.get("https://localhost:7170/Users/WinningUsers");
  const data = await resp.data;
  return data;
};

export const getUsersById = async (id) => {
  const resp = await axios.get(`https://localhost:7170/Users/UserById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchUsers = async (id, put) => {
  const resp = await axios.patch(
    `https://localhost:7170/Users/User/${id}`,
    put
  );
  return await resp.status;
};

export const deleteUsers = async (id) => {
  const resp = await axios.delete(`https://localhost:7170/Users/User/${id}`);
  return await resp.status;
};

export const postUsers = async (post) => {
  const resp = await axios.post("https://localhost:7170/Users/User", post);
  return await resp.status;
};

//votes
export const getVotes = async () => {
  const resp = await axios.get("https://localhost:7170/Votes/AllVotes");
  const data = await resp.data;
  return data;
};

export const getVoteById = async (id) => {
  const resp = await axios.get(`https://localhost:7170/Votes/VoteById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchVotes = async (id, put) => {
  const resp = await axios.patch(
    `https://localhost:7170/Votes/Vote/${id}`,
    put
  );
  return await resp.status;
};

export const deleteVotes = async (id) => {
  const resp = await axios.delete(`https://localhost:7170/Votes/Vote/${id}`);
  return await resp.status;
};

export const postVotes = async (post) => {
  const resp = await axios.post("https://localhost:7170/Votes/Vote", post);
  return await resp.status;
};

