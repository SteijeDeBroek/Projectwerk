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
            {images.slice(0, 1).map((c) => (
              <img
                key={"foto" + c.ImageId}
                src={c.Uri}
                height="100px"
                width="150px"
                alt=""
                className="hover:h-68 hover:w-44 cursor-pointer"
              ></img>
            ))}
            {recipes.slice(0, 1).map((c) => (
              <p key={c.name} className="font-sans font-semibold">
                {c.name}
              </p>
            ))}
          </div>
        </div>
        <div id="winnaars" className="border border-black rounded">
          {images.slice(1, 2).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(1, 2).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
        <div id="winnaars" className="border border-black rounded">
          {images.slice(2, 3).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(2, 3).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
        <div id="winnaars" className="border border-black rounded">
          {images.slice(3, 4).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(3, 4).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
      </div>

      <div className="flex items-center h-96  justify-between p-10 mt-5 border border-green-400 bg-green-200  rounded ">
        {competities.slice(1, 2).map((c) => (
          <h2 key={c.name}>{c.name}</h2>
        ))}
        <div id="winnaars" className="border border-black rounded">
          {images.slice(4, 5).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(4, 5).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
        <div id="winnaars" className="border border-black rounded">
          {images.slice(5, 6).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(5, 6).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
        <div id="winnaars" className="border border-black rounded">
          {images.slice(6, 7).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(6, 7).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
        <div id="winnaars" className="border border-black rounded">
          {images.slice(7, 8).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(7, 8).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
      </div>
      <div className="flex items-center justify-between h-96 p-10 mt-5 border border-blue-400 bg-blue-200  rounded ">
        {competities.slice(2, 3).map((c) => (
          <h2 key={c.name}>{c.name}</h2>
        ))}
        <div id="winnaars" className="border border-black rounded">
          {images.slice(8, 9).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(8, 9).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
        <div id="winnaars" className="border border-black rounded">
          {images.slice(9, 10).map((c) => (
            <img
              key={"foto" + c.ImageId}
              src={c.Uri}
              height="100px"
              width="150px"
              alt=""
              className="hover:h-68 hover:w-44 cursor-pointer"
            ></img>
          ))}
          {recipes.slice(9, 10).map((c) => (
            <p key={c.name} className="font-sans font-semibold">
              {c.name}
            </p>
          ))}
        </div>
        {images.slice(10, 11).map((c) => (
          <img
            key={"foto" + c.ImageId}
            src={c.Uri}
            height="100px"
            width="150px"
            alt=""
            className="hover:h-68 hover:w-44 cursor-pointer"
          ></img>
        ))}
        {recipes.slice(10, 11).map((c) => (
          <p key={c.name} className="font-sans font-semibold">
            {c.name}
          </p>
        ))}
      </div>
      <div id="winnaars" className="border border-black rounded">
        {images.slice(11, 12).map((c) => (
          <img
            key={"foto" + c.ImageId}
            src={c.Uri}
            height="100px"
            width="150px"
            alt=""
            className="hover:h-68 hover:w-44 cursor-pointer"
          ></img>
        ))}
        {recipes.slice(11, 12).map((c) => (
          <p key={c.name} className="font-sans font-semibold">
            {c.name}
          </p>
        ))}
      </div>
    </div>
  );
};

export default CompetitiesComponent;
