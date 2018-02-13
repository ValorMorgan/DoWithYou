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
                <NavHeader />
                <Misc.ClearFix />
                <NavContent/>
            </div>
        );
    }
}

class NavHeader extends React.PureComponent<{}, {}> {
    toggleExpanded() {
        const nav = $('#nav');

        if (nav && nav.attr('class') === 'nav--expanded')
            nav.attr('class', '');
        else if (nav)
            nav.attr('class', 'nav--expanded');
    }

    render() {
        return (
            <div id='nav__header'>
                <Link to={'/'}>
                    <Title>Do With You</Title>
                </Link>
                <Button id='nav__toggler' onClick={this.toggleExpanded}>
                    <Misc.Icon icon="list"></Misc.Icon>
                </Button>
            </div>
        );
    }
}

class NavContent extends React.Component<{}, {}> {
    render() {
        return (
            <div id='nav__content' className='js-nav__content'>
                <DigitalClock/>
                <Misc.ClearFix />
                <NavMenuLink to={'/'} exact icon='home'>Home</NavMenuLink>
                <NavMenuLink to={'/counter'} icon='school'>Counter</NavMenuLink>
                <NavMenuLink to={'/fetchdata'} icon='computer'>Fetch data</NavMenuLink>
            </div>
        );
    }
}

interface INavBarLinkProps extends NavLinkProps {
    icon: string;
}

class NavMenuLink extends React.Component<INavBarLinkProps, {}> {
    constructor(props: INavBarLinkProps) {
        super(props);
    }

    render() {
        const { icon, ...other } = this.props;

        return (
            <NavLink {...other} className={`nav__link ${this.props.className ? this.props.className : ''}`.trim()} activeClassName="active">
                <Misc.Icon icon={icon} />
                <p className="nav__link-content">{this.props.children}</p>
            </NavLink>
        );
    }
}