import { Component } from "react";
import WinnaarsComponent from "./WinnaarsComponent";

class CompetitiesBoxComponent extends Component {
  render() {
    // console.log(this.props); // Debugging
    let Winnaars = [];
    for (let i = 0; i < 4; i++) {
      Winnaars.push(
        <WinnaarsComponent recipe={this.props.recipes[i]} key={"Winnaar" + i} />
      );
    }
    return (
      <div
        className={`flex items-center justify-between h-96 p-10 border ${this.props.borderColor} ${this.props.backgroundColor} rounded`}
        key={"Category" + this.props.competitie.categoryId}
      >
        <h2 key={"Category" + this.props.competitie.categoryId}>
          {this.props.competitie.name}
        </h2>
        {Winnaars}
      </div>
    );
  }
}

export default CompetitiesBoxComponent;
