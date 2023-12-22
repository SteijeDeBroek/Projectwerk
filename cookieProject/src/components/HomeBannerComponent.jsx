import bannerImage from "../assets/banner.png";

const HomeBannerComponent = () => {
  return (
    <div className="flex items-center justify-center   rounded max-h-56 mt-10 mb-10  px-4 lg:px-10">
      <img
        src={bannerImage}
        className="max-h-56 w-full object-cover"
        alt="Banner"
      />
    </div>
  );
};

export default HomeBannerComponent;
