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
                    <NavBarHeader />
                    <Misc.ClearFix />
                    <NavBarContent/>
                </div>
            </div>
        );
    }
}

class NavBarHeader extends React.PureComponent<{}, {}> {
    render() {
        return (
            <div className='navbar-header'>
                <Link className='navbar-brand' to={'/'}>
                    <Title id='title-nav'>Do With You</Title>
                </Link>
                <NavBarButton/>
            </div>
        );
    }
}

class NavBarButton extends React.PureComponent<{}, {}> {
    render() {
        return (
            <button type='button' className='navbar-toggler' data-toggle='collapse' data-target='.navbar-collapse'>
                <Misc.Icon icon="list"></Misc.Icon>
            </button>
        );
    }
}

class NavBarContent extends React.Component<{}, {}> {
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
            <div className='navbar-nav'>
                <NavBarLink to={'/'} exact icon='home'>Home</NavBarLink>
                <NavBarLink to={'/counter'} icon='school'>Counter</NavBarLink>
                <NavBarLink to={'/fetchdata'} icon='computer'>Fetch data</NavBarLink>
            </div>
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
            <NavLink {...other} className={`nav-item nav-link ${this.props.className ? this.props.className : ''}`.trim()} activeClassName="active">
                <Misc.Icon icon={icon} />
                <p className="nav-link-content">{this.props.children}</p>
            </NavLink>
        );
    }
}