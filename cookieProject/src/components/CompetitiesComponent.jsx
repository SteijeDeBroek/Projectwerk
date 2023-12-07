/* eslint-disable no-unused-vars */
import React, { useEffect, useState } from "react";
import "../css/Competities.css"; /* Hij leest de css file niet? */
import {
  getLastThreeCategories,
  getWinningImages,
  getWinningRecipes,
  getWinningUsers,
} from "../API";
import CompetitiesBoxComponent from "./CompetitiesBoxComponent";

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
      <CompetitiesBoxComponent
        competitie={competities[0]}
        images={images.slice(0, 4)}
        recipes={recipes.slice(0, 4)}
        borderColor="border-orange-400"
        backgroundColor="bg-orange-300"
      />
      <CompetitiesBoxComponent
        competitie={competities[1]}
        images={images.slice(4, 8)}
        recipes={recipes.slice(4, 8)}
        borderColor="border-green-400"
        backgroundColor="bg-green-300"
      />
      <CompetitiesBoxComponent
        competitie={competities[2]}
        images={images.slice(8, 12)}
        recipes={recipes.slice(8, 12)}
        borderColor="border-blue-400"
        backgroundColor="bg-blue-300"
      />
    </div>
  );
};

export default CompetitiesComponent;
