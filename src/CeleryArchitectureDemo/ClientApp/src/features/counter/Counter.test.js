import React from "react";
import { render, fireEvent } from "@testing-library/react";
import { Counter } from "./Counter.js";
import { act } from "react-dom/test-utils";

function renderCounter(props) {
  let utils;
  const children = jest.fn(stateAndHelpers => {
    utils = stateAndHelpers;
    return null;
  });
  return {
    ...render(<Counter {...props}>{children}</Counter>),
    children,
    ...utils
  };
}

it("Counter starts at 0 and increments", () => {
  let childrenTest
  act(() => {
    const { children, increment } = renderCounter()
    childrenTest = children
    increment()
  })
  
  expect(childrenTest).toHaveBeenNthCalledWith(1, expect.objectContaining({count: 0}))
  expect(childrenTest).toHaveBeenNthCalledWith(2, expect.objectContaining({count: 1}))
});
