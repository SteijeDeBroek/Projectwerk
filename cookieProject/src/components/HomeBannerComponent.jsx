import React from "react";

const HomeBannerComponent = () => {
  return (
    <div className="flex items-center justify-center border border-blue-400 bg-blue-200 rounded max-h-56 mt-10 mb-10">
      <img
        src="https://scontent-bru2-1.xx.fbcdn.net/v/t1.6435-9/75279382_2696071330500416_7725147415191224320_n.jpg?_nc_cat=101&ccb=1-7&_nc_sid=2be8e3&_nc_ohc=qeAvvyTz15YAX_Pc-N5&_nc_ht=scontent-bru2-1.xx&oh=00_AfCr4zgRoIyBrz1uNzj8sPmXp2bdsF1h58aPLMOXLCQxbg&oe=6575BFA7"
        className="max-h-56 w-full object-cover"
        alt="Banner"
      />
    </div>
  );
};

export default HomeBannerComponent;
