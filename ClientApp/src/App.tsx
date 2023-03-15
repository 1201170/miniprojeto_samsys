import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './routes/AppRoutes';
import { Layout } from './components/Layout';
import './css/custom.css';

export default class App extends React.Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </Layout>
    );
  }
}
