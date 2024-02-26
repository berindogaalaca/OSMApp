import React from 'react'

const Input = ({
    labelFor,
    labelText,
    inputType,
    placeholder,
    inputName,
    onInputChange,
    value,
    inputid, ref
}) => {
    const handleInputChange = (e) => {
        const inputValue = e.target.value;
        onInputChange && onInputChange(e);
    };
    return (
        <div className="mb-2">
          <label htmlFor={labelFor}>{labelText}</label>
          <div className="input-group">
                <input
                    ref={ref }
                    id={inputid }
              type={inputType}
              placeholder={placeholder}
              className="form-control"
              onChange={handleInputChange}
              name={inputName}
              value={value}
            />
          </div>
        </div>
      );
}

export default Input