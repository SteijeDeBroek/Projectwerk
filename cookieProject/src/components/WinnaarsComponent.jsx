import { Component } from "react";
import { getImageById } from "../api";

class WinnaarsComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      image: null,
      isLoading: true,
    };
  }

  componentDidMount() {
    const imageId = this.props.recipe.imageIds[0];

    getImageById(imageId)
      .then((imageData) => {
        this.setState({
          image: imageData,
          isLoading: false,
        });
      })
      .catch((error) => {
        console.error("Error fetching image:", error);
        this.setState({
          isLoading: false,
        });
      });
  }

  render() {
    const { isLoading } = this.state;

    if (isLoading) {
      return <div>Loading...</div>;
    }

    const imageStyle = {
      backgroundImage: `url(data:image/jpg;base64,${this.state.image.base64Image})`,
      backgroundSize: "cover",
      backgroundPosition: "center",
      height: "200px",
      width: "250px",
      display: "flex",
      flexDirection: "column", // Stack items vertically
      justifyContent: "flex-end", // Align items to the bottom
    };

    return (
      <div
        id="winnaars"
        className="border border-black rounded"
        key={"Winnaar" + this.props.recipe.recipeId}
        style={imageStyle}
      >
        <p
          key={"Recipe" + this.props.recipe.recipeId}
          className="font-sans font-semibold text-white"
          style={{
            fontFamily: "Arial",
            textShadow: "1px 1px 2px black", // Aanpassing hier voor de text-shadow
          }}
        >
          {this.props.recipe.title}
        </p>
      </div>
    );
  }
}

export default WinnaarsComponent;
