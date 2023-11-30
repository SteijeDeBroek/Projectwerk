import axios from "axios"

//definieer de functies
//categories
export const getCategories = async () => {
return await axios.get("")
}
export const deleteCategories = async () => {
return await axios.delete("")
}

export const postCategories = async (post) => {
return await axios.post("", post)
}
//images
export const getImages = async () => {
return await axios.get("")
}

export const deleteImages = async () => {
return await axios.delete("")
}

export const postImages = async (post) => {
return await axios.post("", post)
}
//recipes
export const getRecipes = async () => {
return await axios.get("")
}
export const deleteRecipes = async () => {
return await axios.delete("")
}
export const postRecipes = async (post) => {
return await axios.post("", post)
}
//todos
export const getTodos = async () => {
return await axios.get("")
}
export const deleteTodos = async () => {
return await axios.delete("")
}

export const postTodos = async (post) => {
return await axios.post("",post)
}
//Users
export const getUsers = async () => {
return await axios.get("")
}
export const deleteUsers = async () => {
return await axios.delete("")
}

export const postUsers = async (post) => {
return await axios.post("",post)
}
//votes
export const getVotes = async () => {
return await axios.get("")
}
export const deleteVotes = async () => {
return await axios.delete("")
}

export const postVotes = async (post) => {
return await axios.post("",post)
}