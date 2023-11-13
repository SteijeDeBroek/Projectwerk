import React from "react";

const HomeBanner = () => {
  return (
    <div className="p-10">
      <div className="flex items-center justify-center p-10 border border-blue-400 bg-blue-200 rounded max-h-56">
        <img
          src="https://scontent-bru2-1.xx.fbcdn.net/v/t1.6435-9/75279382_2696071330500416_7725147415191224320_n.jpg?_nc_cat=101&ccb=1-7&_nc_sid=2be8e3&_nc_ohc=qeAvvyTz15YAX_Pc-N5&_nc_ht=scontent-bru2-1.xx&oh=00_AfCr4zgRoIyBrz1uNzj8sPmXp2bdsF1h58aPLMOXLCQxbg&oe=6575BFA7"
          className="p-0 max-h-56 w-full object-cover"
          alt="Banner"
        />
      </div>
    </div>
  );
};

export default HomeBanner;
