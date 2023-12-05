/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import "../css/Competities.css"; /* Hij leest de css file niet? */
import {
  getLastThreeCategories,
  getWinningImages,
  getWinningRecipes,
  getWinningUsers,
} from "../API";

const CompetitiesComponent = () => {
  const [competities, setCompetities] = useState([]);
  const [images, setImages] = useState([]);
  const [recipes, setRecipes] = useState([]);
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await getWinningUsers();
        setUsers(response);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchUsers();
  }, []);

  useEffect(() => {
    const fetchRecipes = async () => {
      try {
        const response = await getWinningRecipes();
        setRecipes(response);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchRecipes();
  }, []);

  useEffect(() => {
    const fetchWinningImages = async () => {
      try {
        const response = await getWinningImages();
        setImages(response);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchWinningImages();
  }, []);

  useEffect(() => {
    const fetchCompetities = async () => {
      try {
        const response = await getLastThreeCategories();
        setCompetities(response);
      } catch (err) {
        console.error("error:", err);
      }
    };
    fetchCompetities();
  }, []);

  return (
    <div className="pl-40">
      <div className="flex items-center justify-between h-96 p-10  border border-orange-400 bg-orange-300 rounded">
        {competities.slice(0, 1).map((c) => (
          <h2 key={c.name}>{c.name}</h2>
        ))}
        <div id="winnaars" className="border border-black rounded">
          <div id="winnaars" className="limit">
            <img
              src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            />
            <p className="font-sans font-semibold">
              Naam Gerecht 1 met lasagna
            </p>
          </div>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht 2</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht 3</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht 4</p>
        </div>
      </div>

      <div className="flex items-center h-96  justify-between p-10 mt-5 border border-green-400 bg-green-200  rounded ">
        {competities.slice(1, 2).map((c) => (
          <h2 key={c.name}>{c.name}</h2>
        ))}
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="150px"
            alt=""
            className="hover:h-68 hover:w-44 cursor-pointer"
          />
          <p className="font-sans font-semibold">Naam Gerecht</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht</p>
        </div>
      </div>
      <div className="flex items-center justify-between h-96 p-10 mt-5 border border-blue-400 bg-blue-200  rounded ">
        {competities.slice(2, 3).map((c) => (
          <h2 key={c.name}>{c.name}</h2>
        ))}
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="150px"
            alt=""
            className="hover:h-68 hover:w-44 cursor-pointer"
          />
          <p className="font-sans font-semibold">Naam Gerecht</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht</p>
        </div>
        <div id="winnaars" className="border border-black rounded">
          <img
            src="https://scontent-bru2-1.xx.fbcdn.net/v/t39.30808-6/260417119_4675057169268479_4379047456459630175_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5f2048&_nc_ohc=iKU7FLYcby8AX-9x4ip&_nc_ht=scontent-bru2-1.xx&oh=00_AfBuYa6GThfN067epjVJKK7OvIgNdEzhSLVbRQYZ3lTzjQ&oe=6558C9EF"
            height="100px"
            width="120px"
            alt=""
            className="hover:h-60 hover:w-36 cursor-pointer"
          />
          <p>Naam Gerecht</p>
        </div>
      </div>
    </div>
  );
};

export default CompetitiesComponent;
