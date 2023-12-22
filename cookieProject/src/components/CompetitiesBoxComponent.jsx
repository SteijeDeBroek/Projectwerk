import { Component } from "react";
import WinnaarsComponent from "./WinnaarsComponent";

class CompetitiesBoxComponent extends Component {
  render() {
    // console.log(this.props); // Debugging
    let Winnaars = [];
    for (let i = 0; i < 4; i++) {
      Winnaars.push(
        <WinnaarsComponent
          recipe={this.props.recipes[i]}
          key={"Winnaar" + i}
          position={i}
          borderColor={this.props.borderColor}
        />
      );
    }
    return (
      <div
        className={`mb-10 border ${this.props.borderColor} rounded-l-2xl ${this.props.backgroundColor}`}
        key={"Category" + this.props.competitie.categoryId}
      >
        <h2
          className="capitalize "
          key={"Category" + this.props.competitie.categoryId}
        >
          {this.props.competitie.name}
        </h2>
        <div className="flex items-center justify-between h-96 p-10">
          {Winnaars}
        </div>
      </div>
    );
  }
}

export default CompetitiesBoxComponent;
