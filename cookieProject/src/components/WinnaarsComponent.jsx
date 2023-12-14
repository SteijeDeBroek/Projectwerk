import { Component } from "react";
import { getImageById } from "../api";

class WinnaarsComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      image: null,
      isLoading: true, // Added loading state
    };
  }

  componentDidMount() {
    const imageId = this.props.recipe.imageIds[0];

    // Fetch image data using the promise
    getImageById(imageId)
      .then((imageData) => {
        // Update state with the fetched image data and set isLoading to false
        this.setState({
          image: imageData,
          isLoading: false,
        });
      })
      .catch((error) => {
        console.error("Error fetching image:", error);
        // Set isLoading to false in case of an error
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

    return (
      <div
        id="winnaars"
        className="border border-black rounded"
        key={"Winnaar" + this.props.recipe.recipeId}
      >
        <img
          key={"Image" + this.props.recipe.imageIds[0]}
          src={`data:image/jpg;base64,${this.state.image.base64Image}`}
          height="100px"
          width="150px"
          alt="base64Image"
          className="hover:h-68 hover:w-44 cursor-pointer"
        ></img>
        <p
          key={"Recipe" + this.props.recipe.recipeId}
          className="font-sans font-semibold"
        >
          {this.props.recipe.title}
        </p>
      </div>
    );
  }
}

export default WinnaarsComponent;
