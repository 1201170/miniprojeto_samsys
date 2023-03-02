import * as React from "react";
import BookInterface from '../BookInterface'
export default class BookList extends React.Component{
constructor (props: BookInterface){
  super(props);
}
render() {
  return (  
    <div>
      <h1>Book List</h1>
        <h2>The list of all books</h2>
    </div>
    );
  }
}
