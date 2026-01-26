import { Navigate, Outlet } from 'react-router-dom';


interface RoleRouteProps {
  allowedRoles: string[];
}

export function RoleRoute({ allowedRoles }: RoleRouteProps) {
  const userString = localStorage.getItem('user');
  const user = userString ? JSON.parse(userString) : null;

  if (!user) {
    return <Navigate to="/login" replace />;
  }

  // Normalize role from backend (roles array) or fallback
  let rawRole = user.roles && user.roles.length > 0 ? user.roles[0] : (user.role || 'seller');
  const userRole = rawRole.toLowerCase();

  if (!allowedRoles.includes(userRole)) {
    return <Navigate to={`/${userRole}/dashboard`} replace />;
  }

  return <Outlet />;
}
