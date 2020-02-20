import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-auth/LoginMenu'
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);
    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    }
  };

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/"> MyRental </NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                
                <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle nav caret className="text-dark">
                    My Items
                  </DropdownToggle>
                  <DropdownMenu right>
                    <DropdownItem tag={Link} className="text-dark" to="/mylist">
                      My Item List
                    </DropdownItem>
                    <DropdownItem tag={Link} className="text-dark" to="/myarchivedlist">
                      Archived Item List
                    </DropdownItem>
                    <DropdownItem divider />
                    <DropdownItem>
                      Placeholder
                    </DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>

                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/newitem">New Item</NavLink>
                </NavItem>
                <LoginMenu></LoginMenu>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    )
  }
}