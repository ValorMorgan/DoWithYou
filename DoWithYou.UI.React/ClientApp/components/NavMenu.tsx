import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { DigitalClock } from './Clock';
import { Title } from './Title'

export class NavBarButton extends React.Component<{}, {}> {
    render() {
        return (
            <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                <span className='sr-only'>Toggle navigation</span>
                <span className='icon-bar'></span>
                <span className='icon-bar'></span>
                <span className='icon-bar'></span>
            </button>
        );
    }
}

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return (
            <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                    <div className='navbar-header'>
                        <NavBarButton />
                        <Link className='navbar-brand' to={'/'}>
                            <Title id='title-nav'>Do With You</Title>
                        </Link>
                    </div>
                    <div className='clearfix'></div>
                    <div className='navbar-collapse collapse'>
                        <DigitalClock />
                        <div className='clearfix'></div>
                        <ul className='nav navbar-nav'>
                            <li>
                                <NavLink to={ '/' } exact activeClassName='active'>
                                    <span className='glyphicon glyphicon-home'></span> Home
                                </NavLink>
                            </li>
                            <li>
                                <NavLink to={ '/counter' } activeClassName='active'>
                                    <span className='glyphicon glyphicon-education'></span> Counter
                                </NavLink>
                            </li>
                            <li>
                                <NavLink to={ '/fetchdata' } activeClassName='active'>
                                    <span className='glyphicon glyphicon-th-list'></span> Fetch data
                                </NavLink>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
}
