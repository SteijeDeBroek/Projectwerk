import { Component } from "react";

class WinnaarsComponent extends Component {
  render() {
    return (
      <div
        id="winnaars"
        className="border border-black rounded"
        key={"Winnaar" + this.props.recipe.recipeId}
      >
        <img
          key={"Image" + this.props.image.imageId}
          src={`data:image/jpg;base64,${this.props.image.base64Image}`}
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
