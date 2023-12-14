import bannerImage from "../assets/banner.jpg";

const HomeBannerComponent = () => {
  return (
    <div className="flex items-center justify-center border border-blue-400 bg-blue-200 rounded max-h-56 mt-10 mb-10">
      <img
        src={bannerImage}
        className="max-h-56 w-full object-cover"
        alt="Banner"
      />
    </div>
  );
};

export default HomeBannerComponent;
