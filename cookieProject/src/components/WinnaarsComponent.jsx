import { Component } from "react";
import { getImageById } from "../api";

class WinnaarsComponent extends Component {
  constructor(props) {
    super(props);
    this.state = {
      image: null,
      isLoading: true,
      error: null,
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
          error: error,
        });
      });
  }

  render() {
    const { isLoading } = this.state;

    if (isLoading) {
      return <div>Loading...</div>;
    }

    if (this.state.error) {
      return <div>Error: {this.state.error.message}</div>;
    }

    const imageStyle = {
      backgroundImage: `linear-gradient(rgba(0, 0, 0, 0) 25%, rgba(0, 0, 0, 0.9)), url(data:image/jpg;base64,${this.state.image.base64Image})`,
      backgroundSize: "cover",
      backgroundPosition: "center",
      height: this.props.position == 0 ? "250px" : "200px",
      width: this.props.position == 0 ? "300px" : "250px",
      boxShadow: this.props.position == 0 ? "0 0 50px gold" : "none",
      padding: "10px",
      display: "flex",
      flexDirection: "column",
      justifyContent: "flex-end",
    };

    return (
      <div
        id="winnaars"
        className={`border ${
          this.props.position == 0
            ? "border-yellow-300"
            : this.props.borderColor
        } rounded-2xl hover:shadow-2xl transition duration-500 ease-in-out transform hover:-translate-y-1 hover:scale-110 ${
          this.props.position == 0 ? "" : "hover:border-white"
        }`}
        key={"Winnaar" + this.props.recipe.recipeId}
        style={imageStyle}
      >
        <p
          key={"Recipe" + this.props.recipe.recipeId}
          className="text-white font-semibold capitalize"
          style={{
            fontFamily: "Arial",
          }}
        >
          {this.props.recipe.title}
        </p>
      </div>
    );
  }
}

export default WinnaarsComponent;
