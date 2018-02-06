import * as React from 'react';
import * as Misc from './Utilities/Misc';
import { Link, NavLink, NavLinkProps } from 'react-router-dom';
import { DigitalClock } from './Utilities/Clock';
import { Title } from './Utilities/Title';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return (
            <div className='nav'>
                <div className='navbar'>
                    <NavBarHeader/>
                    <Misc.ClearFix />
                    <NavBarCollapse/>
                </div>
            </div>
        );
    }
}

class NavBarHeader extends React.PureComponent<{}, {}> {
    render() {
        return (
            <div className='navbar-header'>
                <NavBarButton/>
                <Link className='navbar-brand' to={'/'}>
                    <Title id='title-nav'>Do With You</Title>
                </Link>
            </div>
        );
    }
}

class NavBarButton extends React.PureComponent<{}, {}> {
    render() {
        return (
            <button type='button' className='navbar-toggler' data-toggle='collapse' data-target='.navbar-collapse'>
                <span className='sr-only'>Toggle navigation</span>
                <span className='icon-bar'></span>
                <span className='icon-bar'></span>
                <span className='icon-bar'></span>
            </button>
        );
    }
}

class NavBarCollapse extends React.Component<{}, {}> {
    render() {
        return (
            <div className='navbar-collapse collapse'>
                <DigitalClock/>
                <Misc.ClearFix />
                <NavBarLinkList/>
            </div>
        );
    }
}

class NavBarLinkList extends React.Component<{}, {}> {
    render() {
        return (
            <ul className='navbar-nav'>
                <li className='nav-item'><NavBarLink to={'/'} exact icon='glyphicon-home'>Home</NavBarLink></li>
                <li className='nav-item'><NavBarLink to={'/counter'} icon='glyphicon-education'>Counter</NavBarLink></li>
                <li className='nav-item'><NavBarLink to={'/fetchdata'} icon='glyphicon-th-list'>Fetch data</NavBarLink></li>
            </ul>
        );
    }
}

interface INavBarLinkProps extends NavLinkProps {
    icon: string;
}

class NavBarLink extends React.Component<INavBarLinkProps, {}> {
    constructor(props: INavBarLinkProps) {
        super(props);
    }

    render() {
        const { icon, ...other } = this.props;

        return (
            <NavLink {...other} className={`nav-link ${this.props.className ? this.props.className : ''}`.trim()} activeClassName="active">
                <Misc.Icon {...icon} /> {this.props.children}
            </NavLink>
        );
    }
}