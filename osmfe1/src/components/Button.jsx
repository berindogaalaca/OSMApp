import React from 'react';

function Button({ buttontext, buttonclick, buttonStyle, btnid, ref }) {
  return (
      <button className="button btn-ligth rounded-3 mx-3" id={btnid} style={buttonStyle} onClick={buttonclick} ref={ ref}>{buttontext}</button>
  );
}

export default Button;