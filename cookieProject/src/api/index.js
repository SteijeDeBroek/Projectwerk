import axios from "axios";

const baseURL = "https://localhost:7170/";

const axiosInstance = axios.create({
  baseURL,
  headers: {
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,POST,DELETE,PATCH",
  },
});

export const getCategories = async () => {
  const resp = await axiosInstance.get(baseURL + "Categories/GetAsync");
  const data = await resp.data;
  return data;
};

export const getMostRecentCategories = async (amount) => {
  const resp = await axiosInstance.get(
    baseURL + `Categories/GetMostRecentAsync?amount=${amount}`
  );
  const data = await resp.data;
  return data;
};

export const getCategoryById = async (id) => {
  const resp = await axiosInstance.get(baseURL + `Categories/${id}`);
  const data = await resp.data;
  return data;
};

export const patchCategories = async (id, patch) => {
  const resp = await axiosInstance.patch(baseURL + `Categories/${id}`, patch);
  return await resp.status;
};

export const deleteCategories = async (id) => {
  const resp = await axiosInstance.delete(baseURL + `Categories/${id}`);
  return await resp.status;
};

export const postCategories = async (post) => {
  const resp = await axiosInstance.post(baseURL + "Categories", post);
  return await resp.status;
};

export const getImages = async () => {
  const resp = await axiosInstance.get(baseURL + "Images/GetAsync");
  const data = await resp.data;
  return data;
};

export const getImageById = async (id) => {
  const resp = await axiosInstance.get(baseURL + `Images/${id}`);
  const data = await resp.data;
  return data;
};

export const getRandomImageByRecipeId = async (id) => {
  const resp = await axiosInstance.get(baseURL + `Recipes/RandomImage/${id}`);
  const data = await resp.data;
  return data;
};

export const patchImages = async (id, put) => {
  const resp = await axiosInstance.patch(baseURL + `Images/${id}`, put);
  return await resp.status;
};

export const deleteImages = async (id) => {
  const resp = await axiosInstance.delete(baseURL + `Images/${id}`);
  return await resp.status;
};

export const postImages = async (post) => {
  const resp = await axiosInstance.post(baseURL + "Images", post);
  return await resp.status;
};

export const getRecipes = async () => {
  const resp = await axiosInstance.get(baseURL + "Recipes/GetAsync");
  const data = await resp.data;
  return data;
};

export const getSortedWinningRecipes = async (id, amount = 4) => {
  const resp = await axiosInstance.get(baseURL + `Categories/${id}-${amount}`);
  const data = await resp.data;
  return data;
};

export const getRecipeById = async (id) => {
  const resp = await axiosInstance.get(baseURL + `Recipes/${id}`);
  const data = await resp.data;
  return data;
};

export const patchRecipes = async (id, put) => {
  const resp = await axiosInstance.patch(baseURL + `Recipes/${id}`, put);
  return await resp.status;
};

export const deleteRecipes = async (id) => {
  const resp = await axiosInstance.delete(baseURL + `Recipes/${id}`);
  return await resp.status;
};

export const postRecipes = async (post) => {
  const resp = await axiosInstance.post(baseURL + "Recipes", post);
  return await resp.status;
};

export const getTodos = async () => {
  const resp = await axiosInstance.get(baseURL + "Todos/AllTodos");
  const data = await resp.data;
  return data;
};

export const getTodoById = async (recipeId, userId) => {
  const resp = await axiosInstance.get(
    baseURL + `Todos/TodoById/${recipeId}-${userId}`
  );
  const data = await resp.data;
  return data;
};

export const deleteTodos = async (recipeId, userId) => {
  const resp = await axiosInstance.delete(
    baseURL + `Todos/Todo/${recipeId}-${userId}`
  );
  return await resp.status;
};

export const postTodos = async (post) => {
  const resp = await axiosInstance.post(baseURL + "Todos/Todo", post);
  return await resp.status;
};

export const getUsers = async () => {
  const resp = await axiosInstance.get(baseURL + "Users/AllUsers");
  const data = await resp.data;
  return data;
};

export const getUserById = async (id) => {
  const resp = await axiosInstance.get(baseURL + `Users/UserById/${id}`);
  const data = await resp.data;
  return data;
};

export const deleteUsers = async (id) => {
  const resp = await axiosInstance.delete(baseURL + `Users/User/${id}`);
  return await resp.status;
};

export const postUser = async (post) => {
  const resp = await axiosInstance.post(baseURL + "Users/User", post);
  return await resp.status;
};

export const getTodosByUserId = async (userId) => {
  const resp = await axiosInstance.get(baseURL + `Users/UserTodos/${userId}`);
  return resp.data;
};

export const getVotes = async () => {
  const resp = await axios.get(baseURL + "votes/AllVotes");
  const data = await resp.data;
  return data;
};

export const getVoteById = async (recipeId, userId) => {
  const resp = await axios.get(
    baseURL + `votes/VoteById/${recipeId}-${userId}`
  );
  const data = await resp.data;
  return data;
};

export const deleteVote = async (recipeId, userId) => {
  const resp = await axios.delete(baseURL + `votes/Vote/${recipeId}-${userId}`);
  return await resp.status;
};

export const postVote = async (post) => {
  const resp = await axios.post(baseURL + "votes/Vote", post);
  return await resp.status;
};

export const addUpvoteToRecipe = async (recipeId, userId) => {
  const resp = await axiosInstance.post(
    baseURL + `Recipes/${recipeId}/Upvote/${userId}`
  );
  return resp.data;
};

export const addDownvoteToRecipe = async (recipeId, userId) => {
  const resp = await axiosInstance.post(
    baseURL + `Recipes/${recipeId}/Downvote/${userId}`
  );
  return resp.data;
};
