import { useMemo, useState } from 'react';
import { useEmployees } from '../api/get-employees';
import { useNavigate } from 'react-router-dom';
import { Center, Pagination, Paper, Table } from '@mantine/core';
import { paths } from '@/config/paths';

export const EmployeeList = () => {
  const [page, setPage] = useState<number>(1);
  const employeesQuery = useEmployees({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (employeesQuery.data == null) {
      return 0;
    }

    return Math.ceil(employeesQuery.data.totalItems / employeesQuery.data.itemsPerPage);
  }, [employeesQuery.data]);

  if (employeesQuery.isLoading) {
    return <div>loading...</div>;
  }

  const employees = employeesQuery.data?.items;
  if (!employees) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Employee ID</Table.Th>
              <Table.Th>First Name</Table.Th>
              <Table.Th>Last Name</Table.Th>
              <Table.Th>Email</Table.Th>
              <Table.Th>Phone Number</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {employees.map((employee) => (
              <Table.Tr key={employee.id} onClick={() => navigate(paths.businessManagement.updateEmployee.getHref(employee.id))}>
                <Table.Td>#{employee.id}</Table.Td>
                <Table.Td>{employee.firstName}</Table.Td>
                <Table.Td>{employee.lastName}</Table.Td>
                <Table.Td>{employee.email}</Table.Td>
                <Table.Td>{employee.phoneNumber}</Table.Td>
              </Table.Tr>
            ))}
          </Table.Tbody>
        </Table>
      </Paper>

      <Center>
        <Pagination total={totalPages} value={page} onChange={setPage} mt="md" />
      </Center>
    </>
  );
};