import {BookComponent} from '../components/BookComponent';
import BookList from '../components/BookList';
import { Counter } from '../components/Counter';
import { Home } from '../components/Home';


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/book-list',
    element: <BookList />
  },
  {
    path: '/counter',
    element: <Counter />
  },
];

export default AppRoutes;