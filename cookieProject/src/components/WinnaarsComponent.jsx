import { Component } from "react";

class WinnaarsComponent extends Component {
  render() {
    return (
      <div id="winnaars" className="border border-black rounded">
        <img
          key={"Image" + this.props.image.imageId}
          src={this.props.image.uri}
          height="100px"
          width="150px"
          alt="img"
          className="hover:h-68 hover:w-44 cursor-pointer"
        ></img>
        <p key={this.props.recipe.recipeId} className="font-sans font-semibold">
          {this.props.recipe.title}
        </p>
      </div>
    );
  }
}

export default WinnaarsComponent;
