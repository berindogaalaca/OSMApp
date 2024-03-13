import React from 'react'

const Button = ({buttontext,buttonclick,btnid}) => {
  return (
    <button className="button rounded-3 ms-3  px-4 bg-dark text-white"  style={{ width: '100%', height: '5vh' }}  id={btnid} onClick={buttonclick}>{buttontext}</button>
  )
}

export default Button