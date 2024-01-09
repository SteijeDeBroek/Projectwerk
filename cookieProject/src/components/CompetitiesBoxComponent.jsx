import { Component } from "react";
import WinnaarsComponent from "./WinnaarsComponent";

class CompetitiesBoxComponent extends Component {
  render() {
    const headerBgColors = [
      "bg-green-400",
      "bg-teal-400",
      "bg-indigo-400",
      "bg-purple-400",
      "bg-pink-400",
      "bg-red-400",
      "bg-orange-400",
      "bg-amber-400",
      "bg-lime-400",
      "bg-yellow-400",
    ];

    const headerColor = headerBgColors[this.props.index];

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
        className={`mb-10 border-4 ${this.props.borderColor} rounded-l-2xl ${this.props.backgroundColor} border-r-0`}
        key={"Category" + this.props.competitie.categoryId}
      >
        <h2
          className={`capitalize text-white text-7xl font-bold p-10 ${headerColor}`}
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
