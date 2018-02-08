import * as React from 'react';
import * as $ from 'jquery';
import * as Misc from './Utilities/Misc';
import { Link, NavLink, NavLinkProps } from 'react-router-dom';
import { DigitalClock } from './Utilities/Clock';
import { Title } from './Utilities/Title';
import { Button } from './Utilities/Button';

export class NavMenu extends React.Component<{}, {}> {
    render() {
        return (
            <div id='nav'>
                <NavBarHeader />
                <Misc.ClearFix />
                <NavBarContent/>
            </div>
        );
    }
}

class NavBarHeader extends React.PureComponent<{}, {}> {
    render() {
        return (
            <div className='nav-header'>
                <Link to={'/'}>
                    <Title>Do With You</Title>
                </Link>
                <NavBarButton/>
            </div>
        );
    }
}

class NavBarButton extends React.PureComponent<{}, {}> {
    render() {
        return (
            <Button className='nav-toggler' onClick={() => $('.nav-collapse').slideToggle()}>
                <Misc.Icon icon="list"></Misc.Icon>
            </Button>
        );
    }
}

class NavBarContent extends React.Component<{}, {}> {
    render() {
        return (
            <div className='nav-collapse'>
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
            <React.Fragment>
                <NavBarLink to={'/'} exact icon='home'>Home</NavBarLink>
                <NavBarLink to={'/counter'} icon='school'>Counter</NavBarLink>
                <NavBarLink to={'/fetchdata'} icon='computer'>Fetch data</NavBarLink>
            </React.Fragment>
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
                <Misc.Icon icon={icon} />
                <p className="nav-link-content">{this.props.children}</p>
            </NavLink>
        );
    }
}