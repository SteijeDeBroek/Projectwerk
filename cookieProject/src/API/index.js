import axios from "axios"

//tijd ervoor en erna loggen (tip wim!)

//definieer de functies
//categories
export const getCategories = async () => {
return await axios.get("https://localhost:7170/Categories/AllCategories")
}

export const getCategoryById = async (id) => {
return await axios.get(`https://localhost:7170/Categories/CategoryById/${id}`)
}

export const patchCategories = async (id, put) => {
return await axios.patch(`https://localhost:7170/Categories/Category/${id}`,put)
}

export const deleteCategories = async (id) => {
return await axios.delete(`https://localhost:7170/Categories/Category/${id}`)
}

export const postCategories = async (post) => {
return await axios.post("https://localhost:7170/Categories/Category", post)
}
//images
export const getImages = async () => {
return await axios.get("https://localhost:7170/Images/AllImages")
}
export const getImageById = async (id) => {
return await axios.get(`https://localhost:7170/Images/ImageById/${id}`)
}
export const patchImages = async (id,put) => {
return await axios.patch(`https://localhost:7170/Images/Image/${id}`, put)
}
export const deleteImages = async (id) => {
return await axios.delete(`https://localhost:7170/Images/Image/${id}`)
}

export const postImages = async (post) => {
return await axios.post("https://localhost:7170/Images/Image", post)
}
//recipes
export const getRecipes = async () => {
return await axios.get("https://localhost:7170/Recipes/AllRecipes")
}
export const getRecipeById = async (id) => {
return await axios.get(`https://localhost:7170/Recipes/RecipeById/${id}`)
}
export const patchRecipes = async (id,put) => {
return await axios.patch(`https://localhost:7170/Recipes/Recipe/${id}`, put)
}
export const deleteRecipes = async (id) => {
return await axios.delete(`https://localhost:7170/Recipes/Recipe/${id}`)
}
export const postRecipes = async (post) => {
return await axios.post("https://localhost:7170/Recipes/Recipe", post)
}
//todos
export const getTodos = async () => {
return await axios.get("https://localhost:7170/Todos/AllTodos")
}
export const getTodoById = async (id) => {
return await axios.get(`https://localhost:7170/Todos/TodoById/${id}`)
}
export const patchTodos = async (id, put) => {
return await axios.patch(`https://localhost:7170/Todos/Todo/${id}`, put)
}
export const deleteTodos = async (id) => {
return await axios.delete(`https://localhost:7170/Todos/Todo/${id}`)
}

export const postTodos = async (post) => {
return await axios.post("https://localhost:7170/Todos/Todo", post)
}
//Users
export const getUsers = async () => {
return await axios.get("https://localhost:7170/Users/AllUsers")
}
export const getUsersById = async (id) => {
return await axios.get(`https://localhost:7170/Users/UserById/${id}`)
}
export const patchUsers = async (id, put) => {
return await axios.patch(`https://localhost:7170/Users/User/${id}`, put)
}
export const deleteUsers = async (id) => {
return await axios.delete(`https://localhost:7170/Users/User/${id}`)
}

export const postUsers = async (post) => {
return await axios.post("https://localhost:7170/Users/User",post)
}
//votes
export const getVotes = async () => {
return await axios.get("https://localhost:7170/Votes/AllVotes")
}
export const getVoteById = async (id) => {
return await axios.get(`https://localhost:7170/Votes/VoteById/${id}`)
}
export const patchVotes = async (id, put) => {
return await axios.patch(`https://localhost:7170/Votes/Vote/${id}`, put)
}
export const deleteVotes = async (id) => {
return await axios.delete(`https://localhost:7170/Votes/Vote/${id}`)
}

export const postVotes = async (post) => {
return await axios.post("https://localhost:7170/Votes/Vote",post)
}