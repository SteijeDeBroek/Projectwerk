import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getImageById } from "../api";
import goldMedal from "../assets/gold.png";
import silverMedal from "../assets/silver.png";
import bronzeMedal from "../assets/bronze.png";

const WinnaarsComponent = (props) => {
  const [image, setImage] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [medal, setMedal] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const imageId = props.recipe.imageIds[0];
    const position = props.position;

    switch (position) {
      case 0:
        setMedal(goldMedal);
        break;
      case 1:
        setMedal(silverMedal);
        break;
      case 2:
        setMedal(bronzeMedal);
        break;
      default:
        break;
    }

    getImageById(imageId)
      .then((imageData) => {
        setImage(imageData);
        setIsLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching image:", error);
        setIsLoading(false);
        setError(error);
      });
  }, [props.position, props.recipe.imageIds]);

  const navigateToRecipe = () => {
    navigate(`/recipe/${props.recipe.recipeId}`);
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  const imageStyle = {
    backgroundImage: `linear-gradient(rgba(0, 0, 0, 0) 25%, rgba(0, 0, 0, 0.9)), url(data:image/jpg;base64,${image.base64Image})`,
    backgroundSize: "cover",
    backgroundPosition: "center",
    height: props.position === 0 ? "250px" : "200px",
    width: props.position === 0 ? "300px" : "250px",
    boxShadow: props.position === 0 ? "0 0 50px gold" : "none",
    padding: "10px",
    display: "flex",
    flexDirection: "column",
    justifyContent: "flex-end",
  };

  return (
    <div
      id="winnaars"
      className={`border ${
        props.position === 0 ? "border-yellow-300" : props.borderColor
      } rounded-2xl hover:shadow-2xl transition duration-500 ease-in-out transform hover:-translate-y-1 hover:scale-110 ${
        props.position === 0 ? "" : "hover:border-white"
      }`}
      key={"Winnaar" + props.recipe.recipeId}
      style={imageStyle}
      onClick={navigateToRecipe}
    >
      <p
        key={"Recipe" + props.recipe.recipeId}
        className="text-white font-semibold capitalize"
        style={{
          fontFamily: "Arial",
          maxWidth: "80%",
        }}
      >
        {props.recipe.title}
      </p>
      {medal && (
        <img
          src={medal}
          alt="medal"
          className="w-24 h-24"
          style={{
            position: "absolute",
            bottom: 0,
            right: 0,
            transform: "translate(50%, 50%)",
          }}
        />
      )}
    </div>
  );
};

export default WinnaarsComponent;
