import * as React from "react";
import BookInterface from '../../models/Book/Book'
export class BookComponent extends React.Component<BookInterface, {}> {
constructor (props: BookInterface){
  super(props);
}
render() {
  return (  
    <div>
      <h1>Book Component</h1>
        Isbn:  <b>{this.props.bookIsbn}</b>
        <br/>
        Author: <b>{this.props.bookAuthor} </b>
        <br/>
        Name: <b>{this.props.bookName}</b>
        <br/>
        Price: <b>{this.props.bookPrice.toString()}</b>
    </div>
    );
  }
}
