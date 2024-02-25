import React from 'react';

const BackArrow = () => {
  return (
    <div className="back-arrow">

      <a href="./HomePage.jsx">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="24"
        height="24"
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      >
        <path d="M19 12H6M12 5l-7 7 7 7" />
      </svg>
      </a>
    </div>
  );
};

export default BackArrow;
