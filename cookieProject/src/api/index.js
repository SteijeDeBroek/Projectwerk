import axios from "axios";

//tijd ervoor en erna loggen (tip wim!)
const baseURL = "https://localhost:7170/";

//definieer de functies
// categories
export const getCategories = async () => {
  const resp = await axios.get(baseURL + "Categories/AllCategories");
  const data = await resp.data;
  return data;
};

export const mostRecentCategories = async (amount) => {
  console.time("mostRecentCategories_TimerStart");

  const resp = await axios.get(
    baseURL + `Categories/MostRecentCategories?amount=${amount}`
  );

  const data = await resp.data;

  console.timeEnd("mostRecentCategories_TimerEnd");
  return data;
};

export const getCategoryById = async (id) => {
  const resp = await axios.get(baseURL + `Categories/CategoryById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchCategories = async (id, patch) => {
  const resp = await axios.patch(baseURL + `Categories/Category/${id}`, patch);
  return await resp.status;
};

export const deleteCategories = async (id) => {
  const resp = await axios.delete(baseURL + `Categories/Category/${id}`);
  return await resp.status;
};

export const postCategories = async (post) => {
  const resp = await axios.post(baseURL + "Categories/Category", post);
  return await resp.status;
};

//images
export const getImages = async () => {
  const resp = await axios.get(baseURL + "Images/AllImages");
  const data = await resp.data;
  return data;
};

export const getWinningImages = async () => {
  const resp = await axios.get(baseURL + "Images/WinningImages"); // images van gerechten met de meeste votes weergeven per category
  const data = await resp.data;
  return data;
};

export const getImageById = async (id) => {
  const resp = await axios.get(baseURL + `Images/ImageById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchImages = async (id, put) => {
  const resp = await axios.patch(baseURL + `Images/Image/${id}`, put);
  return await resp.status;
};

export const deleteImages = async (id) => {
  const resp = await axios.delete(baseURL + `Images/Image/${id}`);
  return await resp.status;
};

export const postImages = async (post) => {
  const resp = await axios.post(baseURL + "Images/Image", post);
  return await resp.status;
};

//recipes
export const getRecipes = async () => {
  const resp = await axios.get(baseURL + "Recipes/AllRecipes");
  const data = await resp.data;
  return data;
};

export const getWinningRecipes = async () => {
  const resp = await axios.get(baseURL + "Recipes/WinningRecipes");
  const data = await resp.data;
  return data;
};

export const getRecipeById = async (id) => {
  const resp = await axios.get(baseURL + `Recipes/RecipeById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchRecipes = async (id, put) => {
  const resp = await axios.patch(baseURL + `Recipes/Recipe/${id}`, put);
  return await resp.status;
};

export const deleteRecipes = async (id) => {
  const resp = await axios.delete(baseURL + `Recipes/Recipe/${id}`);
  return await resp.status;
};

export const postRecipes = async (post) => {
  const resp = await axios.post(baseURL + "Recipes/Recipe", post);
  return await resp.status;
};

//todos
export const getTodos = async () => {
  const resp = await axios.get(baseURL + "Todos/AllTodos");
  const data = await resp.data;
  return data;
};

export const getTodoById = async (recipeId, userId) => {
  const resp = await axios.get(
    baseURL + `Todos/TodoById/${recipeId}-${userId}`
  );
  const data = await resp.data;
  return data;
};

export const patchTodos = async (recipeId, userId, put) => {
  const resp = await axios.patch(
    baseURL + `Todos/Todo/${recipeId}-${userId}`,
    put
  );
  return await resp.status;
};

export const deleteTodos = async (recipeId, userId) => {
  const resp = await axios.delete(baseURL + `Todos/Todo/${recipeId}-${userId}`);
  return await resp.status;
};

export const postTodos = async (post) => {
  const resp = await axios.post(baseURL + "Todos/Todo", post);
  return await resp.status;
};

//Users
export const getUsers = async () => {
  const resp = await axios.get(baseURL + "Users/AllUsers");
  const data = await resp.data;
  return data;
};

export const getWinningUsers = async () => {
  const resp = await axios.get(baseURL + "Users/WinningUsers");
  const data = await resp.data;
  return data;
};

export const getUsersThatAlreadyVoted = async () => {
  const resp = await axios.get(baseURL + "Users/AlreadyVoted");
  const data = await resp.data;
  return data;
};

export const getUsersById = async (id) => {
  const resp = await axios.get(baseURL + `Users/UserById/${id}`);
  const data = await resp.data;
  return data;
};

export const patchUsers = async (id, put) => {
  const resp = await axios.patch(baseURL + `Users/User/${id}`, put);
  return await resp.status;
};

export const deleteUsers = async (id) => {
  const resp = await axios.delete(baseURL + `Users/User/${id}`);
  return await resp.status;
};

export const postUsers = async (post) => {
  const resp = await axios.post(baseURL + "Users/User", post);
  return await resp.status;
};

//votes
export const getVotes = async () => {
  const resp = await axios.get(baseURL + "Votes/AllVotes");
  const data = await resp.data;
  return data;
};

export const getVoteById = async (recipeId, userId) => {
  const resp = await axios.get(
    baseURL + `Votes/VoteById/${recipeId}-${userId}`
  );
  const data = await resp.data;
  return data;
};

export const patchVotes = async (id, put) => {
  const resp = await axios.patch(baseURL + `Votes/Vote/${id}`, put);
  return await resp.status;
};

export const deleteVotes = async (id) => {
  const resp = await axios.delete(baseURL + `Votes/Vote/${id}`);
  return await resp.status;
};

export const postVotes = async (post) => {
  const resp = await axios.post(baseURL + "Votes/Vote", post);
  return await resp.status;
};
